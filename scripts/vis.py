import csv
import pandas as pd
import matplotlib.pyplot as plt
import argparse
            
def read_pandas(filename):
    df = pd.read_csv(filename, sep=';', names=['x', 'y', 'id'])   
    df.plot.scatter(x="x", y="y", c = 'id', colormap='viridis')
    plt.show()

if __name__ == "__main__":
    parser = argparse.ArgumentParser(description = "")
    parser.add_argument("db_path", metavar = "db_path", type = str,
                        help ="Path to file containing clustered points")
    args = parser.parse_args()
    read_pandas(args.db_path)
    